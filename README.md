# Sensing Gestures

> A tool for gesture-based interactions with smartphone sensors.

The idea of gestures here means patterns of bi-dimensional coordinates over time. To use gesture recognition, some template gestures are stored in a training set that is later used to compare and classify gestures, giving each try a match with a score. As long as you can map two float values to the gesture recorder and recognizer, you can define gestures from all sorts of input sources.

* If coordinates are attached to an accelerometer sensor, gestures will involve the player moving the phone on a specific way.
* If coordinates are attached to touch position on the phone screen, then gestures will be more similar to drawings on that surface.
* If you use magnetic fields as coordinates, then it will detect specific movements in relation to Earth's magnetic field or a close-by magnet.

This tool was developed and used in the [Red Dirt](https://github.com/enricllagostera/RedDirt/) game project. You can find a more [detailed discussion of this tool's design and process over here](https://github.com/enricllagostera/SensingGestures/wiki/Discussion).

## Features

* Integration with the [$1 Unistroke Gesture Recognizer](http://depts.washington.edu/madlab/proj/dollar/index.html).
* Two workflows: an OSC-based one for prototyping and an native sensor one for builds ([Why two workflows?](https://github.com/enricllagostera/SensingGestures/wiki/OSC-based-prototyping-and-native-sensors)).
* Example interface for recording gestures for later use (on the `SensingGestures_Recording` scene).

## Video overview

Soon.

## Installation

The tool is saved here both as a project and as a unitypackage file. AFter downloading the project, you can copy the contents of the `SensingGestures` folder to your own project or you can double-click the unitypackage and import the files from there.

## Getting started

1. Add the `Gestures` prefab to a scene. It contains a few things to make easier to work with gestures, such as a basic debug user interface to visualize values and gestures, as well as a standard "wiring" of events.
2. Connect a sensor to the `UpdateInputValue` function of the `GestureRecognizer` component. You need to define which sensor you will use in the Inspector window for both the OSC Sensor and Native Sensor game objects. Make sure that both game objects are getting information from the same sensors. To connect, you can use the UnityEvent interface to access the `GestureRecognizer.UpdateInputValue` method (check highlights below).
   + OSC Sensor: Check the [prototyping workflow instructions here](https://github.com/enricllagostera/SensingGestures/wiki/OSC-based-prototyping-and-native-sensors). Use the same port and OSC address value as in the [Sensors2OSC](https://sensors2.org/osc/) app or other OSC-server you are using.
    ![OSC Sensor configuration](https://github.com/enricllagostera/SensingGestures/blob/master/Docs/value-connection-osc.png?raw=true)
   + Native Sensor: Select the sensor and make sure that the `Sensor Reader` and the `Native Sensor Dispatcher` components reference the same values (`Gravity` in the example).
    ![Native Sensor configuration](https://github.com/enricllagostera/SensingGestures/blob/master/Docs/value-connection-native.png?raw=true)
3. Now you can run the scene in the editor to test gesture recording and recognizing using the `Space` and `R` keyboard keys. On the training set file in your Project window, you can change the name of your gestures to something more usable. You can also duplicate it and switch between different sets, if you want.
4. In a script from your game, you can declare a method with the header `public void YourMethodName (Result result)` and connect it to the `OnRecognizedGesture` event on the `GestureRecognizer` component. Your method will then be called with information about the result that you can use in your game logic.

## Contributing

If you have ideas for features or bugs you encountered while using the shellphone project, please [open an issue](https://github.com/enricllagostera/SensingGestures/issues). If you'd like to contribute with code or features yourself, please fork the repository and use a feature branch. Pull requests are warmly welcome.

### Ideas for contributions

* Add other gesture recognizer algorithms, such as the [PDollar recognizer](https://assetstore.unity.com/packages/tools/input-management/pdollar-point-cloud-gesture-recognizer-21660), which supports multistroke gestures (which is great for touchscreen-drawing gestures).
* Prepare an example using touch-position coordinates as inputs.

## Links

* Documentation is available both at the [wiki](https://github.com/enricllagostera/SensingGestures/wiki) and on the `Docs` folder.
* Here you can find more Information about the [$1 unistroke gesture recognizer](http://depts.washington.edu/madlab/proj/dollar/index.html) and the [Unity implementation which I'm using](https://github.com/SteBeeGizmo/DollarUnity).
* Open Sound Control (OSC) is a flexible way of communicating between devices and applications. Sensing Gestures uses the [OscJack](https://github.com/keijiro/OscJack) library for Unity. I recommend using the [Sensors2OSC](https://sensors2.org/osc/) app for accessing smartphone sensors and sending their values over a wifi network.
* To access Android phone sensors beyond accelerometers and touchscreen, I have used the [Android Sensors for Unity](https://github.com/mmeiburg/unityAndroidSensors) library. It is used when building the app for the phone.
