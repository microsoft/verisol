pragma solidity >=0.4.24 <0.6.0;

contract A {
    function foo() public returns (uint) {
        return 1;
    }
}

contract B {
    address myaddr;

    function test(address addr) public {

        A a = A(addr);
        uint b = a.foo();
        assert (b == 1);

        address x = address(0x5); 
        address y = address(a); 
        address z = address(new A());
        assert (z != address(0x0));        

        //annoying case that survived
        myaddr = address(new A());
        assert (myaddr != address(0x0));                
    }
}
