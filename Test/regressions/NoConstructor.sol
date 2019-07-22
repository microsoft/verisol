pragma solidity >=0.4.24 <0.6.0;

interface NoConstructor {
    function activate() external;
    function getAddress() external view returns (address);
}
