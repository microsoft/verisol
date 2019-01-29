pragma solidity ^0.4.24;

contract A {
      uint x;
      int128 x128;
      string y;
      address z;
      bool w;

      constructor() {
          assert(x == 0);
          assert(x128 == 0);
          assert(z == 0x0);
          assert(!w);

          string memory yval = "";
          bytes32 a = keccak256(bytes(y));
          bytes32 b = keccak256(bytes(yval));
          assert(a == b);

          y = "aa";
          a = keccak256(bytes(y));
          assert(a != b);



      }
}
