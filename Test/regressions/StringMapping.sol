pragma solidity ^0.4.24;

contract Mapping {

    mapping (string => uint) m;

    constructor(string s) public {        
        m[s] = 21;    // string memory
        assert (m[s] == 21);

        m["aa"] = 11; // literal string
        assert (m["aa"] == 11);

        string memory aaString = "aa";
        assert (m[aaString] == 11);
    }

}
