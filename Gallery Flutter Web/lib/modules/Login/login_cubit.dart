import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:gallery/modules/Login/login_states.dart';


class LoginCubit extends Cubit<LoginStates> {
  LoginCubit() : super(InitialLoginState());

  static LoginCubit get(context) {
    return BlocProvider.of(context);
  }

  bool isPassVisible = true;
  IconData myIcon = Icons.visibility;

  toggleVisibility() {
    isPassVisible = !isPassVisible;
    if (isPassVisible == true) {
      myIcon = Icons.visibility;
    } else {
      myIcon = Icons.visibility_off;
    }
    emit(ToggleVisibilityLoginState());
  }
  login(String email,String password) async{
    emit(LoadingLoginState());
    try {
      UserCredential userCredential = await FirebaseAuth.instance.signInWithEmailAndPassword(
          email: email,
          password: password
      );
      // toggleVisibility();
      Fluttertoast.showToast(
          msg: "Logging in",
          toastLength: Toast.LENGTH_SHORT,
          gravity: ToastGravity.CENTER,
          timeInSecForIosWeb: 1,
          backgroundColor: Colors.red,
          textColor: Colors.white,
          fontSize: 16.0
      );
      emit(SuccessLoginState());

    } on FirebaseAuthException catch (e) {
      // print(e);
      Fluttertoast.showToast(
          msg: "Incorrect username or password",
          toastLength: Toast.LENGTH_SHORT,
          gravity: ToastGravity.CENTER,
          timeInSecForIosWeb: 1,
          backgroundColor: Colors.red,
          textColor: Colors.white,
          fontSize: 16.0
      );
      emit(ErrorLoginState());

      if (e.code == 'user-not-found') {
        print('No user found for that email.');
      } else if (e.code == 'wrong-password') {
        print('Wrong password provided for that user.');
      }
    }
  }
}
