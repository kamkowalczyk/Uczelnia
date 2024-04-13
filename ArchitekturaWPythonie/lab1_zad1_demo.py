import functools
import time
import logging

logging.basicConfig(level=logging.INFO)

def timer(func):
    @functools.wraps(func)
    def wrapper(*args, **kwargs):
        start = time.time()
        result = func(*args, **kwargs)
        end = time.time()
        logging.info(f"Function {func.__name__} took {end - start} seconds.")
        return result
    return wrapper

@timer
def calculation(n):
    iter_ = 0
    for i in range(n):
        iter_ += i

print(calculation(int (1e6)))

