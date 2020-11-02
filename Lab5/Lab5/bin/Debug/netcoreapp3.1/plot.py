import matplotlib.pyplot as plt

x = []
y = []

f = open('result.txt', 'r')

fileData = f.read().split("\n")

xStr = fileData[1].split(' ')
yStr = fileData[0].split(' ')

for i in range(0, len(xStr)-1):
    x.append(int(xStr[i]))
for i in range(0, len(yStr)-1):
    y.append(int(yStr[i]))

plt.plot(x, y, label='Модель')
plt.ylabel('Time')
plt.xlabel('ProcessCount')
plt.show()
