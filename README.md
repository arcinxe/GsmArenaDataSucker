# GsmArenaDataSucker

This application saves the data of all the devices from the gsmarena.com website.

The app uses this unofficial [GSMArena API](https://github.com/xpresservers/gsm) to make it possible. 

#### Requirements:

- [.NET Core](https://dotnet.microsoft.com/download)
- Running instance of [GSMArena API](https://github.com/xpresservers/gsm)

#### Usage
1. Launch [GSMArena API](https://github.com/xpresservers/gsm#quick-start) using instructions from that link

2. Run GsmArenaDataSucker
```bash
cd GsmArenaDataSucker
dotnet run
# After about one and half an hour of working all details will be saved in AllDevicesDetails.json file
```

#### Info
The result data looks something like this simplified fragment.

```json
{
    "data": {
        "id": "9536",
        "slug": "samsung_galaxy_s10-9536",
        "brand": "Samsung",
        "device_name": "Samsung Galaxy S10",
        "device_type": "Phone",
        "image_url": "https://fdn2.gsmarena.com/vv/bigpic/samsung-galaxy-s10.jpg",
        "detail": {
            "body": {
                "build": "Back glass (Gorilla Glass 5), aluminum frame",
                "dimensions": "149.9 x 70.4 x 7.8 mm (5.90 x 2.77 x 0.31 in)",
                "sim": "Single SIM (Nano-SIM) or Hybrid Dual SIM (Nano-SIM, dual stand-by)",
                "weight": "157 g (5.54 oz)"
            },
            "comms": {
                "bluetooth": "5.0, A2DP, LE, aptX",
                "gps": "Yes, with A-GPS, GLONASS, BDS, GALILEO",
                "nfc": "Yes",
                "radio": "FM radio (USA \u0026 Canada only)",
                "usb": "3.1, Type-C 1.0 reversible connector",
                "wlan": "Wi-Fi 802.11 a/b/g/n/ac/ax, dual-band, Wi-Fi Direct, hotspot"
            },
            "display": {
                "protection": "Corning Gorilla Glass 6",
                "resolution": "1440 x 3040 pixels, 19:9 ratio (~550 ppi density)",
                "size": "6.1 inches, 93.2 cm2 (~88.3% screen-to-body ratio)",
                "type": "Dynamic AMOLED capacitive touchscreen, 16M colors"
            }
        }
    }
}
```
Unfortunately, there are multiple values in almost every key of the returned details. That makes that data not really ready to use. I recommend using [my parser](https://github.com/arcinxe/GsmArenaDataParser) to convert most of the details into its atomic values. 


#### Related projects
[GsmArenaDataParser](https://github.com/arcinxe/GsmArenaDataParser) 

[GSMArena API](https://github.com/xpresservers/gsm#quick-start)