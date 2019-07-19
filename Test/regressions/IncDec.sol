pragma solidity >=0.4.24 <0.6.0;

contract IncDec {

    function test1() public {
        uint a = 10;
        ++a;
        assert (a == 11);
    }
    
    function test2() public {
        uint a = 10;
        a++;
        assert (a == 11);
    }
    
    function test3() public {
        uint a = 10;
        --a;
        assert (a == 9);
    }
    
    function test4() public {
        uint a = 10;
        a--;
        assert (a == 9);
    }
   
    // as expressions
    function test5() public {
       uint a = 10;
       Foo(a++);
       assert (a == 11);       
       Foo(--a);
       assert (a == 10);       
    }

    function test6() public {
       uint a = 10;
       Foo(a--);
       assert (a == 9);       
       Foo(++a);
       assert (a == 10);       
    }

    function Foo(uint x) private {
       assert (x == 10);
    }
}
