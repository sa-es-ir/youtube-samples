# ASP.NET Core: Faster Api with Multi-Layer cache!

Speaking of caching, you may already have an in-memory or Distributed cache (like Redis) in your application but both approaches have some issues.

In-memory cache can't be used when you have distributed systems and multiple instances of your API, since it's memory-specific.
Distributed cache has the issue of I/O operation and network latency.

Can we merge both approaches? Yes for sure!

In this repo, I've tried to show how to implement a multi-layer cache in ASP.NET Core.

## You can watch the video here: 👇
[![Watch the video](https://img.youtube.com/vi/Au94GcJDBxM/hqdefault.jpg)](https://youtu.be/Au94GcJDBxM)
