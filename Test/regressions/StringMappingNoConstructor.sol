pragma solidity >=0.4.24 <0.6.0;

contract Mapping {

    mapping (string => uint) m;

    function test() public {
        m["aa"] = 11;
        m["bb"] = 21;
        assert (m["aa"] == 11);
        assert (m["bb"] == 21);
    }

}
