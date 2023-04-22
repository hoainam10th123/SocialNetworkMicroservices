import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

import 'models/global.dart';


class HomePage extends StatelessWidget{
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Column(
            children: [
              const Text('Day la trang home'),
              ElevatedButton(
                child: const Text("Logout"),
                onPressed: () async {
                  await logout();
                },
              ),
            ],
          ),
        ),
      )
    );
  }

  Future<void> logout() async {
    final logoutUri = Uri.parse(Global.logoutUrl.toString());
    if (await canLaunchUrl(logoutUri)) {
      await launchUrl(logoutUri);
      Global.logoutUrl = null;
      Global.token = null;
    } else {
      throw 'Could not launch ${Global.logoutUrl}';
    }
    closeInAppWebView();
  }
}