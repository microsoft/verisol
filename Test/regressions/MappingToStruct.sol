pragma solidity >=0.4.24 <0.6.0;

contract MappingToStruct {

    struct Point {
        uint x;
        uint y;
    }
    
    mapping (uint => Point) m;

    function testMapping() public {
        Point memory p1 = Point(1, 2);
        Point memory p2 = Point(3, 4);
        m[10] = p1;
        m[20] = p2;
        assert (m[10].x == 1);
        assert (m[10].y == 2);
        assert (m[20].x == 3);
        assert (m[20].y == 4);
    }

}
