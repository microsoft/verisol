pragma solidity ^0.4.24;

contract Mapping {

    mapping (string => uint) m;

    constructor() public {
        m["aa"] = 11;
        m["bb"] = 21;
        assert (m["aa"] == 11);
        assert (m["bb"] == 21);
    }

}
