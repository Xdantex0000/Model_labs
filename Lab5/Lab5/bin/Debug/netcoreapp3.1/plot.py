import matplotlib.pyplot as plt

x = []
y1 = []
y2 = []

f = open('result.txt', 'r')
f2 = open('result2.txt', 'r')

fileData = f.read().split("\n")
fileData2 = f2.read().split("\n")

xStr = fileData[1].split(' ')
y1Str = fileData[0].split(' ')
y2Str = fileData2[0].split(' ')

for i in range(0, len(xStr)-1):
    x.append(int(xStr[i]))
for i in range(0, len(y1Str)-1):
    y1.append(int(y1Str[i]))
for i in range(0, len(y2Str)-1):
    y2.append(int(y2Str[i]))

plt.plot(x, y1, label='firstModel')
plt.plot(x, y2, label='secondModel')
plt.ylabel('Time')
plt.xlabel('Difficulty')
plt.show()
