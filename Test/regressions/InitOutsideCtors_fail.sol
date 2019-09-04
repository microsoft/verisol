pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint a=1;
    uint b;
    bool c=true;
	string s="hello world";
    constructor() public
    {
		assert(a==1);
		assert(c);
		bytes32 x=keccak256(bytes(s));
		string memory s1="hello world";
		bytes32 y = keccak256(bytes(s1));
		assert(x != y);
    }
}