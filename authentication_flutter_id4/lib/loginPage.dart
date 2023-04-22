import 'package:authentication_flutter_id4/HomePage.dart';
import 'package:flutter/material.dart';
import 'package:openid_client/openid_client_io.dart';
import 'package:url_launcher/url_launcher.dart';

import 'models/global.dart';

//<uses-permission android:name="android.permission.QUERY_ALL_PACKAGES" />
//android:usesCleartextTraffic="true"

class LoginPage extends StatefulWidget{
  @override
  State<StatefulWidget> createState() {
    return LoginPageState();
  }
}

class LoginPageState extends State<LoginPage> {
  final String clientId = 'FlutterClient';
  static const String issuer = 'http://10.0.2.2:5077';
  final List<String> scopes = <String>[
    'openid',
    'profile',
    'email',
    'address',
    'roles',
    'userAPI',
    'postAPI',
    'notificationAPI',
    'chatAPI'
  ];

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            children: [
              const Text('Authentication with Identity server 4', style: TextStyle(fontSize: 28, color: Colors.blue),),
              ElevatedButton(onPressed: () async{
                var tokenInfo = await authenticate(Uri.parse(issuer), clientId, scopes);
                chuyenDenTrangHome();
              },
                child: const Text('Login', style: TextStyle(fontSize: 25),),),
            ],
          ),
        ),
      )
    );
  }

  chuyenDenTrangHome(){
    Navigator.push(
      context,
      MaterialPageRoute(builder: (context) => const HomePage()),
    );
  }

  Future<TokenResponse> authenticate(
      Uri uri, String clientId, List<String> scopes) async {
    // create the client
    var issuer = await Issuer.discover(uri);
    var client = Client(issuer, clientId);

    //The plugin url_launcher_android requires Android SDK version 33. compileSdkVersion 33
    // create a function to open a browser with an url
    urlLauncher(String url) async {
      final uri = Uri.parse(url);
      if (await canLaunchUrl(uri)) {
        await launchUrl(uri);
      } else {
        throw 'Could not launch $url';
      }
    }

    // create an authenticator

    var authenticator = Authenticator(
        client,
      scopes: scopes,
      urlLancher: urlLauncher,
      port: 4200
    );

    // starts the authentication
    var c = await authenticator.authorize();
    // close the webview when finished
    closeInAppWebView();

    var res = await c.getTokenResponse();
    Global.logoutUrl = c.generateLogoutUrl().toString();
    Global.token = res.accessToken;
    return res;
  }

  /*Future<void> logout() async {
    final logoutUri = Uri.parse(logoutUrl);
    if (await canLaunchUrl(logoutUri)) {
      await launchUrl(logoutUri);
    } else {
      throw 'Could not launch $logoutUrl';
    }
    await Future.delayed(const Duration(seconds: 1));
    closeInAppWebView();
  }*/
}