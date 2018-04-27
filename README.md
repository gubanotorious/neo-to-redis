# neo-to-redis
Proof of Concept Project to copy the NEO Blockchain to Redis DB and Redis Streams API

This will read blocks from neo-cli and write both raw and deserialized / re-serialized versions of the blocks to Redis DB and Redis Streams API to act as a test harness as well as allow for visualization / testing of the NEO Blockchain.

<p align="center">
  <img src="NeoRedis.JPG" />
</p>

## Requirements
[redis-unstable-docker](https://github.com/gubanotorious/redis-unstable-docker) needs to be installed / running to enable Redis Streams API Support.
