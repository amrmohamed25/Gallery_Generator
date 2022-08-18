// import 'dart:html';
import 'dart:io';
import 'dart:typed_data';

import 'package:dio/dio.dart';
import 'package:file_picker/file_picker.dart';

// import 'package:firebase_database/firebase_database.dart';
import 'package:firebase_storage/firebase_storage.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:gallery/models/ResourcesModel.dart';
import 'package:gallery/models/SettingsModel.dart';
import 'package:gallery/modules/Setup/SetupStates.dart';

import '../../models/TexturesModel.dart';

class SetupCubit extends Cubit<SetupStates> {
  SetupCubit() : super(InitialSetupState());

  static SetupCubit get(context) {
    return BlocProvider.of(context);
  }

  SettingsModel? myModel;
  TextureModel? floorModel;
  TextureModel? wallModel;
  TextureModel? ceilModel;
  ResourcesModel? resourcesModel;
  var heightController = TextEditingController();

  var radiusController = TextEditingController();

  loadCurrentSetup() async {
    resourcesModel = new ResourcesModel();
    emit(LoadingSetupState());
    var dio = Dio();
    Response response = await dio.get(
        "https://gallery-3d1e5-default-rtdb.firebaseio.com/currentSettings.json");
    print(response.data);
    myModel = SettingsModel.fromJson(response.data as Map<String, dynamic>);
    heightController.text = myModel!.height.toString();
    radiusController.text = myModel!.radius.toString();
    emit(ReadSuccessState());
    response = await dio.get(
        "https://gallery-3d1e5-default-rtdb.firebaseio.com/floorTextures.json");
    print(response.data);
    floorModel = TextureModel.fromJson(response.data as Map<String, dynamic>);
    response = await dio.get(
        "https://gallery-3d1e5-default-rtdb.firebaseio.com/ceilTextures.json");
    print(response.data);
    ceilModel = TextureModel.fromJson(response.data as Map<String, dynamic>);
    response = await dio.get(
        "https://gallery-3d1e5-default-rtdb.firebaseio.com/wallTextures.json");
    print(response.data);
    wallModel = TextureModel.fromJson(response.data as Map<String, dynamic>);
    response = await dio.get(
        "https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText/myResources.json");
    print(response.data);
    resourcesModel = ResourcesModel.fromJson(response.data);
    // // if(resourcesModel!=null)
    // int mysize = resourcesModel?.myResources.length.toInt() ?? 0;
    // for(int i=0;i<mysize;i++){
    //   print(resourcesModel?.myResources[i]);
    // }
    // resourcesModel?.myResources.add(
    //     "https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FWW.jpg?alt=media&token=37224c77-3ec1-46e2-ad7b-f568b5a2bdb4");
    // resourcesModel?.myResources.add(
    //     "https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FWW.txt?alt=media&token=be426699-0d80-4349-89fa-3549f30d45f7");
    // resourcesModel?.myResources.add(
    //     "https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FSaulGoodman.png?alt=media&token=13e8b111-ea3b-4259-9ec3-7a58f0b5b356");
    // resourcesModel?.myResources.add(
    //     "https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FSaulGoodman.txt?alt=media&token=b27aba68-5d5b-486c-9d4d-75fbe27282e3");
    // mysize = resourcesModel?.myResources.length.toInt() ?? 0;
    // print(resourcesModel?.myResources.length);
    // Map<String, String> myMap = {};
    // for (int i = 0; i < mysize; i++) {
    //   myMap["$i"] = resourcesModel!.myResources[i];
    // }
    // print(myMap);
    // await dio.put(
    //     "https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText/myResources.json",
    //     data: myMap);
  }

  makeAToast(String str) {
    Fluttertoast.showToast(
        msg: str,
        toastLength: Toast.LENGTH_SHORT,
        gravity: ToastGravity.CENTER,
        timeInSecForIosWeb: 1,
        backgroundColor: Colors.black,
        textColor: Colors.white,
        fontSize: 16.0);
  }

  void updateModel(String model, String url, String textureType) {
    TextureModel refModel;
    if (model == "floor") {
      refModel = floorModel!;
    } else if (model == "ceil") {
      refModel = ceilModel!;
    } else {
      refModel = wallModel!;
    }
    if (textureType == "ao") {
      refModel.ao = url;
    } else if (textureType == "albedo") {
      refModel.albedo = url;
    } else if (textureType == "metallic") {
      refModel.metallic = url;
    } else if (textureType == "height") {
      refModel.height = url;
    } else if (textureType == "normal") {
      refModel.normal = url;
    } else if (textureType == "roughness") {
      refModel.roughness = url;
    }
  }

  void uploadImagesAndTexts() async {
    FilePickerResult? result =
        await FilePicker.platform.pickFiles(allowMultiple: true);

    if (result != null) {
      var dio = Dio();
      emit(WaitingForImagesUploadState());
      result.files.sort((a, b) => a.name.compareTo(b.name));
      List<PlatformFile>? allImages = [];
      List<PlatformFile>? allTexts = [];
      for (int i = 0; i < result.files.length; i++) {
        // result.f
        Uint8List? fileBytes = result.files[i].bytes;
        String fileName = result.files[i].name;
        if (fileName.contains(".txt")) {
          allTexts.add(result.files[i]);
        } else {
          allImages.add(result.files[i]);
        }
      }
      if (allImages.length == allTexts.length) {
        await FirebaseStorage.instance
            .ref("resources/")
            .listAll()
            .then((value) {
          value.items.forEach((element) {
            FirebaseStorage.instance.ref(element.fullPath).delete();
          });
        });
        int j=0;
        int counter=0;
        resourcesModel?.myResources.clear();
        for (int i = 0; i < (allImages.length)*2; i++) {
          PlatformFile temp;
          print(i);
          if(counter==0){
            temp=allImages[j];
          }else{
            counter=-1;
            temp=allTexts[j];
            j++;
          }
          Uint8List? fileBytes = temp.bytes;
          String fileName = temp.name;
          await FirebaseStorage.instance
              .ref("resources/$fileName")
              .putData(fileBytes!)
              .then((p0) async {
            String url = await p0.ref.getDownloadURL();
            resourcesModel?.myResources.add(url);

            makeAToast("${fileName} uploaded Successfully");
          }).catchError((onError) {
            makeAToast("${fileName} wasn't uploaded,Try Again");
          });
          counter++;
        }
        int mysize = resourcesModel?.myResources.length.toInt() ?? 0;
        Map<String,String> myMap={};
        for (int i = 0; i < mysize; i++) {
          myMap["$i"] = resourcesModel!.myResources[i];
        }
        print(myMap);
        await dio.put(
            "https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText/myResources.json",
            data: myMap);
        emit(SuccessUploadState());
      }
      else{
        emit(CancelledUploadState());
        makeAToast("Images aren't equal to texts");
      }
    } else {
      emit(CancelledUploadState());
      makeAToast("User aborted uploading");
    }
  }

  void uploadImage(String wallType, String textureType) async {
    FilePickerResult? result = await FilePicker.platform.pickFiles();
    emit(WaitingForImagesUploadState());

    if (result != null) {
      Fluttertoast.showToast(
          msg: "Uploading Data",
          toastLength: Toast.LENGTH_SHORT,
          gravity: ToastGravity.CENTER,
          timeInSecForIosWeb: 1,
          backgroundColor: Colors.blue,
          textColor: Colors.white,
          fontSize: 16.0);

      // for (int i = 0; i < result.files.length; i++) {
      Uint8List? fileBytes = result.files.first.bytes;
      String fileName = result.files.first.name;
      var dio = Dio();

      // Uint8List? fileBytes = result.files.bytes;
      // String fileName = result.files[i].name;

      if (wallType == "floor") {
        await FirebaseStorage.instance
            .ref("images/floor/$textureType")
            .listAll()
            .then((value) {
          value.items.forEach((element) {
            FirebaseStorage.instance.ref(element.fullPath).delete();
          });
        });
        await FirebaseStorage.instance
            .ref('images/floor/$textureType/$fileName')
            .putData(fileBytes!)
            .then((p0) async {
          String url = await p0.ref.getDownloadURL();

          updateModel("floor", url.toString(), textureType);
          Response response = await dio.put(
              "https://gallery-3d1e5-default-rtdb.firebaseio.com/floorTextures.json",
              data: floorModel?.setMap());
          makeAToast("${fileName} uploaded Successfully");
        }).catchError((onError) {
          makeAToast("${fileName} wasn't uploaded,Try Again");
        });
      } else if (wallType == "ceil") {
        await FirebaseStorage.instance
            .ref("images/ceil/$textureType")
            .listAll()
            .then((value) {
          value.items.forEach((element) {
            FirebaseStorage.instance.ref(element.fullPath).delete();
          });
        });
        await FirebaseStorage.instance
            .ref('images/ceil/$textureType/$fileName')
            .putData(fileBytes!)
            .then((p0) async {
          String url = await p0.ref.getDownloadURL();

          updateModel("ceil", url.toString(), textureType);
          Response response = await dio.put(
              "https://gallery-3d1e5-default-rtdb.firebaseio.com/ceilTextures.json",
              data: ceilModel?.setMap());
          makeAToast("${fileName} uploaded Successfully");
        }).catchError((onError) {
          makeAToast("${fileName} wasn't uploaded,Try Again");
        });
      } else if (wallType == "wall") {
        await FirebaseStorage.instance
            .ref("images/wall/$textureType")
            .listAll()
            .then((value) {
          value.items.forEach((element) {
            FirebaseStorage.instance.ref(element.fullPath).delete();
          });
        });
        await FirebaseStorage.instance
            .ref('images/wall/$textureType/$fileName')
            .putData(fileBytes!)
            .then((p0) async {
          String url = await p0.ref.getDownloadURL();

          updateModel("wall", url.toString(), textureType);
          Response response = await dio.put(
              "https://gallery-3d1e5-default-rtdb.firebaseio.com/wallTextures.json",
              data: wallModel?.setMap());
          makeAToast("${fileName} uploaded Successfully");
        }).catchError((onError) {
          makeAToast("${fileName} wasn't uploaded,Try Again");
        });
      }
      // }
      emit(SuccessUploadState());
    } else {
      emit(CancelledUploadState());
      makeAToast("User aborted uploading");
    }
  }

  void updateData() async {
    if (heightController.text.isEmpty || radiusController.text.isEmpty) {
      String text = "";
      if (heightController.text.isEmpty) {
        text += "Height can't be empty\n";
      }
      if (radiusController.text.isEmpty) {
        text += "Radius can't be empty";
      }
      Fluttertoast.showToast(
          msg: text,
          toastLength: Toast.LENGTH_SHORT,
          gravity: ToastGravity.CENTER,
          timeInSecForIosWeb: 1,
          backgroundColor: Colors.red,
          textColor: Colors.white,
          fontSize: 16.0);
      return;
    }
    emit(LoadingSetupState());
    myModel?.radius = double.parse(radiusController.text);
    myModel?.height = double.parse(heightController.text);
    var dio = Dio();
    Response response = await dio.put(
        "https://gallery-3d1e5-default-rtdb.firebaseio.com/currentSettings.json",
        data: myModel?.setMap());
    Fluttertoast.showToast(
        msg: "Updated Successfully",
        toastLength: Toast.LENGTH_SHORT,
        gravity: ToastGravity.CENTER,
        timeInSecForIosWeb: 1,
        backgroundColor: Colors.red,
        textColor: Colors.white,
        fontSize: 16.0);
    emit(WriteSuccessState());
    print(response);
    print(response.data);

    ///TO DO ADD WriteErrorState
  }

// }
// if (wallType == "floor") {
// await FirebaseStorage.instance
//     .ref("images/floor")
//     .listAll()
//     .then((value) {
// value.items.forEach((element) {
// FirebaseStorage.instance.ref(element.fullPath).delete();
// });
// });
// } else if (wallType == "ceil") {
// await FirebaseStorage.instance
//     .ref("images/ceil")
//     .listAll()
//     .then((value) {
// value.items.forEach((element) {
// FirebaseStorage.instance.ref(element.fullPath).delete();
// });
// });
// } else if (wallType == "wall") {
// await FirebaseStorage.instance
//     .ref("images/wall")
//     .listAll()
//     .then((value) {
// value.items.forEach((element) {
// FirebaseStorage.instance.ref(element.fullPath).delete();
// });
// });
}
