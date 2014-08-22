Xamarin-forms-winrt
===================

Hi, I am a big fan of all Xamarin products and specially to Xamarin Forms. Every people ask this, why there is no Xamarin Forms for Windows Store app !!!
Yes, currently you can use Xamarin Forms for iPad and Android tablet, but not for Windows 8 tablet... ;(

That's why I have made an implementation of Xamarin Forms for Windows Store app (called Xamarin.Forms.WinRT).

This is a first alpha release, you can find all the source code here, and feel free to fork :)

The available controls are:
 - Button
 - Entry
 - Image
 - ListView (10%)
 - NavigationPage (with full navigation)
 - ContentPage
 - Slider
 - StackLayout
 - Switch
 - TabbedPage

Installation
--------------
- Download all the sources
- Add the latest Xamarin.Forms package from nuget to your iOS or Windows Phone or Android projet
- Create a Windows Store App (the blank template)
- Add a reference to Xamarin.Froms.Platform.WinRT project
- Add a reference manualy to Xamarin.Forms.Core and Xamarin.Forms.Xaml, they are in the packages\Xamarin.Forms.1.2.2.6243\lib\portable-win+net45+wp80+MonoAndroid10+MonoTouch10\ folder
- Enjoy :)

Samples
--------------

You can find 3 samples in the Samples directory, they are based on the Xamarin Forms samples (http://developer.xamarin.com/samples/tag/Xamarin.Forms/)
- TabbedPageDemo
- TipCalc
- Todo


About
--------------
I'm sorry in advance for Xamarin, I did'nt find an other namespace instead of Xamarin.Forms.WinRT... :()

 