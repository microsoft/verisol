pragma solidity >=0.4.24 <0.6.0;


contract A {
   uint [2] l;
   bool [2] aa;
   string[3] n;
   uint [2][2] m;
   uint ui;
   int i;
   string s2;
   bool b;
   uint256[] dynArray;

    constructor () public {
        l[0] = 1;
        l[1] = 2;
        delete l[1];
        assert (l[1] == 0); //simple scalar case
 
        aa[0] = true;
        delete aa[0];
        assert (!aa[0]);

        n[0] = "a";
        n[1] = "b";
        string memory s = "";
        delete n[0];
        bytes32 b1 = keccak256(bytes(s));
        bytes32 b2 = keccak256(bytes(n[0])); 
        assert (b1 ==  b2);

        
        delete dynArray;
        assert (dynArray.length == 0);


        delete n;
        assert (n.length == 3);

        i = -10;
        delete i;
        assert (i == 0);
        

        ui = 10;
        delete ui;
        assert (ui == 0);
        
        s2 = "c";
        delete s2;
        assert (keccak256(bytes(s)) == keccak256(bytes(s2)));
        
        b = true;
        delete b;
        assert (b == false);
                
        
/*
        assert (keccak256(bytes(s)) == keccak256(bytes(n[1])));
        m[0][0] = 1;
        m[0][1] = 2;
        m[1][1] = 3;
        m[1][0] = 4;
        delete m[0];
        assert (m[0][0] == 0);
        assert (m[0][1] == 0);
        delete m;
        assert (m[1][1] == 0);
*/
    }
    
}