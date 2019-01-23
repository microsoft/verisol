pragma solidity ^0.4.24;

contract MappingReference {

    mapping (uint => uint) m;

    function test() public {
        mapping (uint => uint) mm = m;
        m[10] = 11;
        m[20] = 21;
        mm[10] = 20;
        assert (m[10] == 20);
        assert (m[20] == 21);
        assert (mm[10] == 20);
        assert (mm[20] == 21);
    }

}
