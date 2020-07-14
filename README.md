```
_____________  _____________________________________________
__  ___/__  / / /__    |__  __ \__  ____/_  ___/_  __ \__  /
_____ \__  /_/ /__  /| |_  /_/ /_  __/  _____ \_  / / /_  /
____/ /_  __  / _  ___ |  _, _/_  /___  ____/ // /_/ /_  /___
/____/ /_/ /_/  /_/  |_/_/ |_| /_____/  /____/ \____/ /_____/

A bad name for a mediocre application.
```


## Disclaimer

This application does not use any kind of encryption for the traffic, i wouldn't recommend using this for machines across the internet at all.

## Point

The entire point of this application is to have a console application that is meant to enable simple messaging between computers.

### Why?

Because i was too lazy to search the exact same thing on google twice.

## How does this work?

This is entierly client to client based, no need for a "server" to connect two computers together. But that also means no "multi-computer" support.

I dont have any plans at this momemt to change this, but i might want to add encryption and multi-computer support later.

### Configuration

Configuration is set in the Configuration.xml file. There is not optional setting even if it says that its a default. The XML parser does not consider an empty field as a default.
