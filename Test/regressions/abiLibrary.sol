pragma solidity ^0.4.24;
 library abiSample {
    function useAbi2(uint256 a, uint256 b) public pure returns(bytes32) {
         bytes32 h = keccak256(abi.encodePacked(a, b));
         return h;
     }
    function useAbi1(uint256 a) public pure  returns(bytes32) {
         bytes32 h = keccak256(abi.encodePacked(a));
         return h;
      }
}
