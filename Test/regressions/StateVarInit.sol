pragma solidity >=0.4.24 <0.6.0;

contract A {
      uint x;
      int128 x128;
      string y;
      address z;
      bool w;

      constructor() public {
          assert(x == 0);
          assert(x128 == 0);
          assert(z == address(0x0));
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
