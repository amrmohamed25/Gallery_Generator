import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/material.dart';
import 'package:gallery/modules/Login/LoginPage.dart';
import 'package:gallery/modules/Setup/SetupPage.dart';
import 'package:gallery/shared/colors.dart';
import 'package:gradient_widgets/gradient_widgets.dart';
import 'dart:html' as html;
import '../shared/constants.dart';

class WelcomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
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
              const SizedBox(
                height: 150,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: [
                  Padding(
                    padding: const EdgeInsets.all(50.0),
                    child: Column(
                      children: const [
                        SizedBox(
                          height: 20,
                        ),
                        GradientText(
                          'Welcome To BA Gallery',
                          style: TextStyle(
                            fontSize: 30.0,
                          ),
                        ),
                        Text("The application has 3 layouts:"),
                        Text("Corridor, Square, Polygon"),
                      ],
                    ),
                  ),
                  if (MediaQuery.of(context).size.width > 600)
                    Container(
                      color: Colors.black,
                      height: 200,
                      width: 1,
                    ),
                  if (MediaQuery.of(context).size.width > 600)
                    Expanded(
                      child: Padding(
                        padding: const EdgeInsets.all(15.0),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children:  [
                            Text(
                              "Steps To Use",
                              style: TextStyle(
                                  fontWeight: FontWeight.bold, fontSize: 16),
                              textAlign: TextAlign.start,
                            ),
                            TextButton(child: Text("Navigate To Gallery"),onPressed: (){html.window.open("https://gallery-3d1e5.web.app/", "test");},),
                            Text(
                                "1.The user should set the parameters by logging in\n2.The game will ask the user to choose which layout he would like to use\nNote: The player should have a folder containing both the photos and text file ex: sample1.png,sample1.txt, also the application accept png,jpg,jpeg only"),
                            // Flexible(child: Text("2.Open this url ://")),
                            // Flexible(child: Text("3.The game will ask the user to choose which layout he would like to use")),
                            // Flexible(child: Text("Note: The player should have a folder containing both the photos and text file ex: sample1.png,sample1.txt, also the application accept png,jpg,jpeg only"))
                          ],
                        ),
                      ),
                    )
                ],
              ),
              if (MediaQuery.of(context).size.width <= 600)
                Padding(
                  padding: const EdgeInsets.all(15.0),
                  child: Column(
                    children: const [
                      Text(
                        "Steps To Use",
                        style: TextStyle(fontWeight: FontWeight.bold),
                        textAlign: TextAlign.start,
                      ),

                      Text(
                          "1.The user should set the parameters by logging in\n2.Open this url :// to start the application\n3.The game will ask the user to choose which layout he would like to use\nNote: The player should have a folder containing both the photos and text file ex: sample1.png,sample1.txt, also the application accept png,jpg,jpeg only"),
                      // Flexible(child: Text("2.Open this url ://")),
                      // Flexible(child: Text("3.The game will ask the user to choose which layout he would like to use")),
                      // Flexible(child: Text("Note: The player should have a folder containing both the photos and text file ex: sample1.png,sample1.txt, also the application accept png,jpg,jpeg only"))
                    ],
                  ),
                ),
              const SizedBox(
                height: 100,
              ),
              Padding(
                padding: const EdgeInsets.all(15.0),
                child: Column(
                  children: [
                    CarouselSlider(
                        items: [
                          buildCarouselItem(
                              "assets/images/corridor.png", context),
                          buildCarouselItem(
                              "assets/images/square.png", context),
                          buildCarouselItem(
                              "assets/images/polygon.png", context),
                        ],
                        options: CarouselOptions(
                          height: 400,
                          aspectRatio: 16 / 9,
                          viewportFraction: 0.8,
                          initialPage: 0,
                          enableInfiniteScroll: true,
                          reverse: false,
                          autoPlay: true,
                          autoPlayInterval: const Duration(seconds: 3),
                          autoPlayAnimationDuration:
                              const Duration(milliseconds: 800),
                          autoPlayCurve: Curves.fastOutSlowIn,
                          enlargeCenterPage: true,
                          scrollDirection: Axis.horizontal,
                        ))
                  ],
                ),
              ),
              const SizedBox(
                height: 100,
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget buildCarouselItem(String url, BuildContext context) {
    return Card(
      semanticContainer: true,
      clipBehavior: Clip.antiAliasWithSaveLayer,
      child: Column(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Expanded(
            child: Image.network(
              url,
              fit: BoxFit.fill,
            ),
          ),
          const SizedBox(
            height: 10,
          ),
          Text(
            url.split("/").last.split(".").first.toUpperCase(),
            style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
          ),
          const SizedBox(
            height: 10,
          )
        ],
      ),
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(10.0),
      ),
      elevation: 5,
      margin: const EdgeInsets.all(10),
    );
  }
}
