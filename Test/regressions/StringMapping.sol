pragma solidity >=0.4.24 <0.6.0;

contract Mapping {

    mapping (string => uint) m;

    constructor(string memory s) public {        
        m[s] = 21;    // string memory
        assert (m[s] == 21);

        m["aa"] = 11; // literal string
        assert (m["aa"] == 11);

        string memory aaString = "aa";
        assert (m[aaString] == 11);
    }

}
