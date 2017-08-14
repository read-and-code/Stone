def fib(n) {
    if n < 2 {
        n
    } else {
        fib(n - 1) + fib(n - 2)
    }
}
time = currentTime()
fib 15
print currentTime() - time + " milliseconds"