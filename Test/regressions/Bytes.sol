pragma solidity ^0.4.24;

contract Bytes {

    bytes public data = "0x3333";
    bytes public empty;

    function clearData(){
        data = "";
    }
}
