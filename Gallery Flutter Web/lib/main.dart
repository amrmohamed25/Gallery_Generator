import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:gallery/modules/WelcomePage.dart';
import 'package:gallery/shared/constants.dart';
import 'package:shared_preferences/shared_preferences.dart';


void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
      options: const FirebaseOptions(
    apiKey: "AIzaSyBVup_uatKfnBl3_eobNZppETgxyiCUqr8",
    appId: "1:899326896937:web:56b9f5e12bceafb8e76538",
    messagingSenderId: "899326896937",
    projectId: "gallery-3d1e5",
    storageBucket: "gallery-3d1e5.appspot.com",
  ));
  prefs = await SharedPreferences.getInstance();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Gallery',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: WelcomePage(),
    );
  }
}
