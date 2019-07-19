pragma solidity >=0.4.24 <0.6.0;

contract Mapping {

    mapping (uint => uint) m;

    function test() public {
        m[10] = 11;
        m[20] = 21;
        assert (m[10] == 11);
        assert (m[20] == 21);
    }

}
