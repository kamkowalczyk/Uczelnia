import datetime
import time

def log_decorator(func):
    def wrapper(*args, **kwargs):
        start_time = time.time()
        result = func(*args, **kwargs)
        end_time = time.time()
        execution_time = end_time - start_time 
        with open("function_log.txt", "a",  encoding="utf-8") as log_file:
            log_file.write(f"{ str(datetime.datetime.now())}: Function {func.__name__}, Time: {execution_time} seconds \n")
        
        return result
    return wrapper

@log_decorator
def example_function(x):
    time.sleep(x) 
    return x

example_function(2)
