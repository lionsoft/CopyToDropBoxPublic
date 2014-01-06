CopyToDropBoxPublic
===================

This is a very simple program to copy image file passed as the first argument to the public dropbox folder and put the public link to the clipboard.

I wrote this program for fast publishing my screenshots via [FastStone Capture](http://www.faststone.org/FSCaptureDetail.htm), but you can use it in others program as you like.


Program Configuration
---------------------
Before using the program you should config it with your **Dropbox User Id**, path to your local **Dropbox Folder** and optional **Subfolder** within the Dropbox Public folder where your images will be copied. 

To make this you have to edit your `.config` file and set corresponding parameters.

**Sample**
```config
  <userSettings>
    <CopyToDropBoxPublic.Properties.Settings>
      <setting name="UserId" serializeAs="String">
        <value>123456</value>
      </setting>
      <setting name="PublicFolder" serializeAs="String">
        <value>E:\DropBox\Public</value>
      </setting>
      <setting name="SubFolder" serializeAs="String">
        <value>TempImg</value>
      </setting>
    </CopyToDropBoxPublic.Properties.Settings>
  </userSettings>
```

Program Usage
-------------
Using of the program is rather simple. Just execute it with the full file path as an argument and this file will be copied to the defined dropbox folder and its public URL will be copied to the clipboard. 

There is one issue, if you process a `.bmp` file with the program it will be converted to `.png` format and renamed to `yyyy-MM-dd_HHmmss.png` where `yyyy` will be replaced to current year, `MM`, `dd`, `HH`, `mm`, `ss` to current month, day, hour, minutes and seconds respectively.

**Sample**
> \> CopyToDropBoxPublic C:\temp\MyImage.jpg 

`C:\temp\MyImage.jpg` will be copied to the `E:\DropBox\Public\TempImg\MyImage.jpg` and in the clipboard will be copied the `http://dl.dropbox.com/u/123456/TempImg/MyImage.jpg` URL.


> \> CopyToDropBoxPublic C:\temp\MyImage.bmp 

`C:\temp\MyImage.bmp` will be copied to the `E:\DropBox\Public\TempImg\2014-01-31_105013.png` and in the clipboard will be copied the `http://dl.dropbox.com/u/123456/TempImg/2014-01-31_105013.png` URL.


How to use with FastStone Capture
---------------------------------

Just add the program as an external editor and after making screenshot use it. Paste the public link to a chat or email.
