pragma solidity >=0.4.24 <0.6.0;

contract A {

   string t;

   function test() public view {        
        string storage s = t;
        bytes32 sb = keccak256(bytes(s));
        bytes32 st = keccak256(bytes(t));
        string memory aa = "a";
        string memory dd = "d";
        bytes32 sa = keccak256(bytes(aa));
        bytes32 sd = keccak256(bytes(dd));
 
        assert (sb == st);
        assert (sa != sd); //not really true as there can be hash collision
    }

    function testNonZeroHash() public 
    {
        string storage s = t;
        bytes32 sb = keccak256(bytes(s));
        assert(sb!=0);
    }

}
