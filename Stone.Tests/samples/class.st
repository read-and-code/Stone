class Position {
    x = y = 0
    def move(nx, ny) {
        x = nx
        y = ny
    }
}
p = Position.new
p.move(3, 4)
p.x = 10
print p.x + p.y
class Position3D extends Position {
    z = 0
    def set(nx, ny, nz) {
        x = nx
        y = ny
        z = nz
    }
}
p = Position3D.new
p.move(3, 4)
print p.x
p.set(5, 6, 7)
print p.z