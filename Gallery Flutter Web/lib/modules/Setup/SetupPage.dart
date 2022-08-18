import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:gallery/modules/Login/LoginPage.dart';
import 'package:gallery/modules/Setup/SetupCubit.dart';
import 'package:gallery/modules/Setup/SetupStates.dart';
import 'package:gallery/modules/WelcomePage.dart';
import 'package:gallery/shared/constants.dart';
import 'package:gradient_widgets/gradient_widgets.dart';

class SetupPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (BuildContext context) {
        return SetupCubit()..loadCurrentSetup();
      },
      child: BlocConsumer<SetupCubit, SetupStates>(
        listener: (context, state) {},
        builder: (context, state) {
          return state is! LoadingSetupState && state is! InitialSetupState
              ? Scaffold(
                  body: MediaQuery.of(context).size.width > 600 &&
                          MediaQuery.of(context).size.height > 400
                      ? SingleChildScrollView(
                        child: Column(
                          children: [
                            Material(
                              elevation: 10,
                              child: Row(
                                children: [
                                  Padding(
                                      padding: const EdgeInsets.all(8.0),
                                      child: TextButton(
                                        onPressed: () {Navigator.pushReplacement(
                                          context,
                                          MaterialPageRoute(
                                              builder: (context) => WelcomePage()),
                                        );  },
                                        child:const GradientText('BA Gallery',
                                            style: TextStyle(
                                              fontSize: 24.0,
                                              fontWeight: FontWeight.bold,
                                            )),
                                      )),
                                  const Spacer(),
                                  if(prefs.getBool("loggedIn")==null)
                                    Padding(
                                      padding: const EdgeInsets.all(8.0),
                                      child: TextButton(
                                          onPressed: () {
                                            Navigator.push(
                                              context,
                                              MaterialPageRoute(
                                                  builder: (context) => LoginPage()),
                                            );
                                          },
                                          child: const Padding(
                                            padding: EdgeInsets.all(8.0),
                                            child: GradientText(
                                              "Login",
                                              style: TextStyle(fontSize: 18),
                                            ),
                                          )),
                                    )
                                ],
                              ),
                            ),
                            SizedBox(height: 50,),
                            Center(
                                child: SizedBox(
                                    height: 500,
                                    width: 500,
                                    child: buildSettingsCard(context, state)),
                              ),
                          ],
                        ),
                      )
                      : SingleChildScrollView(
                        child: Column(
                          children: [
                            Material(
                              elevation: 10,
                              child: Row(
                                children: [
                                  Padding(
                                      padding: const EdgeInsets.all(8.0),
                                      child: TextButton(
                                        onPressed: () {Navigator.pushReplacement(
                                          context,
                                          MaterialPageRoute(
                                              builder: (context) => WelcomePage()),
                                        );  },
                                        child:const GradientText('BA Gallery',
                                            style: TextStyle(
                                              fontSize: 24.0,
                                              fontWeight: FontWeight.bold,
                                            )),
                                      )),
                                  const Spacer(),
                                  if(prefs.getBool("loggedIn")==null)
                                    Padding(
                                      padding: const EdgeInsets.all(8.0),
                                      child: TextButton(
                                          onPressed: () {
                                            Navigator.push(
                                              context,
                                              MaterialPageRoute(
                                                  builder: (context) => LoginPage()),
                                            );
                                          },
                                          child: const Padding(
                                            padding: EdgeInsets.all(8.0),
                                            child: GradientText(
                                              "Login",
                                              style: TextStyle(fontSize: 18),
                                            ),
                                          )),
                                    )
                                  else
                                    Padding(
                                      padding: const EdgeInsets.all(8.0),
                                      child: TextButton(
                                          onPressed: () {
                                            Navigator.push(
                                              context,
                                              MaterialPageRoute(
                                                  builder: (context) => SetupPage()),
                                            );
                                          },
                                          child: const Padding(
                                            padding: EdgeInsets.all(8.0),
                                            child: GradientText(
                                              "Setup",
                                              style: TextStyle(fontSize: 18),
                                            ),
                                          )),
                                    )
                                ],
                              ),
                            ),
                            Center(
                                child: SizedBox(
                                  width: 350,
                                  height: 350,
                                  child: buildSettingsCard(context, state),
                                ),
                              ),
                             ],
                        ),
                      ))
              : const Scaffold(
                  body: Center(
                    child: CircularProgressIndicator(),
                  ),
                );
        },
      ),
    );
  }

  Widget buildSettingsCard(context, state) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(15.0),
        child: SingleChildScrollView(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [

              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text("Height:     "),
                  SizedBox(
                    width: 100,
                    child: TextFormField(
                      autocorrect: false,
                      inputFormatters: <TextInputFormatter>[
                        FilteringTextInputFormatter.allow(RegExp(r'(^\d*\.?\d*)'))
                      ],
                      keyboardType: TextInputType.number,
                      controller: SetupCubit.get(context).heightController,
                      decoration:
                          const InputDecoration(border: OutlineInputBorder()),
                    ),
                  ),
                ],
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  const Text("Radius:     "),
                  SizedBox(
                    width: 100,
                    child: TextFormField(
                      autocorrect: false,
                      inputFormatters: <TextInputFormatter>[
                        FilteringTextInputFormatter.allow(RegExp(r'(^\d*\.?\d*)'))
                      ],
                      keyboardType: TextInputType.number,
                      controller: SetupCubit.get(context).radiusController,
                      decoration:
                          const InputDecoration(border: OutlineInputBorder()),
                    ),
                  )
                ],
              ),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: GradientButton(
                    callback: () {
                      SetupCubit.get(context).updateData();
                    },
                    child: const Text("Update")),
              ),
              state is! WaitingForImagesUploadState
                  ? Column(
                      children: [
                        const SizedBox(
                          height: 20,
                        ),
                        buildExpansionTile("Floor", context),
                        buildExpansionTile("Wall", context),
                        buildExpansionTile("Ceil", context),


                        const SizedBox(
                          height: 20,
                        ),
                        // TextButton(
                        //   onPressed: () {
                        //     SetupCubit.get(context).uploadImage("wall");
                        //   },
                        //   child: const Text("Upload Wall Textures"),
                        // ),
                        const SizedBox(
                          height: 20,
                        ),
                        // TextButton(
                        //   onPressed: () {
                        //     SetupCubit.get(context).uploadImage("ceil");
                        //   },
                        //   child: const Text("Upload Ceiling Textures"),
                        // )
                      ],
                    )
                  : const Center(child: const CircularProgressIndicator()),
              SizedBox(height: 15,),
              state is! WaitingForImagesUploadState?
              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImagesAndTexts();
                },
                child: const Text("Upload Image And Texts"),
              ):SizedBox(height: 10,),
              Text("Note: The .txt files should be equal to images if not it wont be accepted\nAlso each update will erase previous photos",style: TextStyle(color: Colors.red),)

            ],
          ),
        ),
      ),
    );
  }
  Widget buildExpansionTile(String str,context){
    return   ExpansionTile(
      title: Text(str),
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Text("Albedo"),              Spacer(),

              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImage(str.toLowerCase(),"albedo");
                },
                child: const Text("Upload Albedo Texture"),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Text("AO"),
              Spacer(),
              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImage(str.toLowerCase(),"ao");
                },
                child: const Text("Upload AO Texture"),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Text("Normal"),              Spacer(),

              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImage(str.toLowerCase(),"Normal");
                },
                child: const Text("Upload Normal Texture"),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Text("Height"),              Spacer(),

              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImage(str.toLowerCase(),"height");
                },
                child: const Text("Upload Height Texture"),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Text("Metallic"),              Spacer(),

              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImage(str.toLowerCase(),"metallic");
                },
                child: const Text("Upload Metallic Texture"),
              ),
            ],
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Text("Roughness"),              Spacer(),

              ElevatedButton(
                onPressed: () {
                  SetupCubit.get(context).uploadImage(str.toLowerCase(),"roughness");
                },
                child: const Text("Upload Roughness Texture"),
              ),
            ],
          ),
        ),
      ],
    );
  }
}
