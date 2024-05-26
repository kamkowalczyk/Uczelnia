class DisjointSet:
    def __init__(self, size):
        self.parent = list(range(size + 1))
        self.rank = [0] * (size + 1)
        self.component_count = size

    def find(self, x):
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])
        return self.parent[x]

    def union(self, x, y):
        root_x = self.find(x)
        root_y = self.find(y)

        if root_x != root_y:
            if self.rank[root_x] > self.rank[root_y]:
                self.parent[root_y] = root_x
            elif self.rank[root_x] < self.rank[root_y]:
                self.parent[root_x] = root_y
            else:
                self.parent[root_y] = root_x
                self.rank[root_x] += 1
            self.component_count -= 1

    def count_components(self):
        return self.component_count


def main():
    import sys
    input = sys.stdin.read
    data = input().split()

    index = 0
    N = int(data[index])
    index += 1
    M = int(data[index])
    index += 1

    ds = DisjointSet(N)

    for _ in range(M):
        A = int(data[index])
        index += 1
        B = int(data[index])
        index += 1
        ds.union(A, B)

    print(ds.count_components())

if __name__ == "__main__":
    main()
