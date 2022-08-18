import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:gallery/modules/Login/login_cubit.dart';
import 'package:gallery/modules/Login/login_states.dart';

import 'package:gallery/modules/Setup/SetupPage.dart';
import 'package:gallery/modules/WelcomePage.dart';


import 'package:gallery/shared/constants.dart';
import 'package:gradient_widgets/gradient_widgets.dart';


class LoginPage extends StatelessWidget {
  var emailController = TextEditingController();
  var passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
        create: (BuildContext context) {
          return LoginCubit();
        },
        child: BlocConsumer<LoginCubit, LoginStates>(
          listener: (BuildContext context, Object? state) {
            if (state is SuccessLoginState) {
              prefs.setBool("loggedIn", true);
              Navigator.push(
                context,
                MaterialPageRoute(builder: (context) => SetupPage()),
              );
            }
          },
          builder: (context, state) {
            return Scaffold(
              body: SafeArea(
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
                    const Spacer(),
                    SizedBox(
                      width: MediaQuery.of(context).size.width / 2,
                      child: Card(
                        child: Column(
                          children: [
                            Container(
                              width: double.infinity,
                              height: 25,
                              decoration: const BoxDecoration(
                                  gradient: Gradients.hotLinear,
                                  borderRadius: BorderRadius.only(
                                      topLeft: Radius.circular(5),
                                      topRight: Radius.circular(5))),
                              child: const Text(
                                "LOGIN",
                                textAlign: TextAlign.center,
                                style: TextStyle(color: Colors.white),
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(15.0),
                              child: TextField(
                                controller: emailController,
                                decoration: const InputDecoration(
                                    border: OutlineInputBorder(),
                                    labelText: 'Email',
                                    hintText: 'Enter Your Email'),
                              ),
                            ),
                            Padding(
                              padding: const EdgeInsets.all(15.0),
                              child: TextField(
                                controller: passwordController,
                                obscureText:
                                    LoginCubit.get(context).isPassVisible,
                                decoration: InputDecoration(
                                    border: const OutlineInputBorder(),
                                    labelText: 'Password',
                                    hintText: 'Enter Password',
                                    suffixIcon: IconButton(
                                        onPressed: () {
                                          LoginCubit.get(context)
                                              .toggleVisibility();
                                        },
                                        icon: Icon(
                                            LoginCubit.get(context).myIcon))),
                              ),
                            ),
                            if (state is LoadingLoginState)
                              const Padding(
                                padding: EdgeInsets.all(8.0),
                                child: CircularProgressIndicator(),
                              )
                            else

                              Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: GradientButton(
                                    callback: emailController.text.isNotEmpty&& passwordController.text.isNotEmpty?() {

                                      LoginCubit.get(context).login(
                                          emailController.text,
                                          passwordController.text);
                                      // LoginCubit.get(context).toggleVisibility();
                                      // print("lol");
                                    }:(){},
                                    child: const Text("LOGIN")),
                              )
                          ],
                        ),
                      ),
                    ),
                    const Spacer(),
                  ],
                ),
              ),
            );
          },
        ));
  }
}
