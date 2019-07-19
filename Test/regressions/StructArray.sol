pragma solidity >=0.4.24 <0.6.0;

// struct arrays and storage

contract StructType {

    struct S {
        uint x;
        uint y;
    }

   S s;
   S s1;
   S[] ss;

    function testStructType() public {
        s.x = 1;
        s.y = 2;
        assert (s.x == 1);
        assert (s.y == 2);
    }

    function localStructs() public {
         S memory t;
         s.x = 1;
         t = s; //copy
         t.x = 2;
         assert (s.x == 1);
    }
   
     function structArray() public {
           S memory t = S(1, 2);
           ss.push(t);
           assert (ss[0].x == 1);
           ss[0].x = 2;
           assert (ss[0].x == 2);
           t = ss[0];
           t.x = 33;
           assert (ss[0].x == 2);
           structStorage();
      }

     function structStorage() private {
           S storage t = ss[0]; //reference
           ss[0].x = 2;
           assert (ss[0].x == 2);
           t.x = 3; 
           assert (ss[0].x == 3);
      }

     function structStateVarAssign() public {
          s.x = 1;
          s1 = s;
          s1.x = 2;
          assert (s.x == 1);
      }

}
