pragma solidity >=0.4.24 <0.6.0;

contract MappingNested {

    mapping (uint => mapping (uint => uint)) m;
    mapping (uint => uint[]) n;
    uint[] p;
  

    constructor() public {
        // assert(m[0][22] == 0); 

        m[10][20] = 11;
        m[20][10] = 21;
        assert (m[10][20] == 11);
        assert (m[20][10] == 21);
        assert (n[0].length == 0);
        n[0].push(22);
        n[0].push(33);
        assert (n[0].length == 2);
        assert (n[1].length == 0); //should pass but fails
        assert (p.length == 0); //should pass but fails
        //assert (false); //reachable
    }

}
