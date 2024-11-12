# WebView2Map
Sample for PTV Developer within a WinForms application

Demonstrates how to:
* Utilize [WebView2](https://developer.microsoft.com/microsoft-edge/webview2) to display the PTV Developer [Vector Maps API](https://developer.myptv.com/en/documentation/vector-maps-api).
* Integrate interactive routing using the PTV Developer [Routing Api](https://developer.myptv.com/en/documentation/routing-api) and [maplibre-gl-directions](https://github.com/maplibre/maplibre-gl-directions).
* Bridge api calls between Winforms and WebView2.

Get your free API key at https://developer.myptv.com/ and add it in [Form1.cs](https://github.com/oliverheilig/WebView2Map/blob/main/Form1.cs).

**Note:** It seems sometimes the WebView2 runtimes folder isn't copied at the initial build. In this case just clean and rebuild the solution.

![screenshot](https://raw.githubusercontent.com/oliverheilig/WebView2Map/main/screenshot.png)
