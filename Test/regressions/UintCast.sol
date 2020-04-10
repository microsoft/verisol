pragma solidity >=0.5.0;

contract Test {
    function test(address addr) public pure returns (uint) {
        uint addrInt = uint(addr);
        int x2 = int(addr);
        int32 x3 = int32(addr);
        uint32 x4 = uint32(addr);
        // x4 = uint32(addrInt); // unhandled between different int types
        address y = address(x2);
        return addrInt;
    }
}