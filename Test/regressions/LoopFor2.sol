pragma solidity >=0.4.24 <0.6.0;
contract LoopFor2 {
    uint[100] b;
    uint[100] c;

    constructor () public {
      b[0] = c[0];
    }
    function testUnboundedForLoop(uint n) public {
      require (n > 0 && n < 100);
      for (uint i = 0; i < n; i += 1) {
        b[i] = i + 1;
        if (n > 3) { c[i] = b[i] - 1; }
        else c[i] = b[i];
      }
    }
    function test() public { assert(b[0] == c[0]); }
}
