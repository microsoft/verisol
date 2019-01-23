pragma solidity ^0.4.24;

interface NoConstructor {
    function activate() external;
    function getAddress() external view returns (address);
}
