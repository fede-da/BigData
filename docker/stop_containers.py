import os
import subprocess

def start_containers(parent_dir="."):
    processes = []
    # List all items in the directory
    for item in os.listdir(parent_dir):
        # Construct full path
        dir_path = os.path.join(parent_dir, item)
        # Check if it is a directory
        if os.path.isdir(dir_path):
            # Define the path to the docker-compose.yml file
            compose_file = os.path.join(dir_path, 'docker-compose.yaml')
            # Print the expected path for debugging
            print(f"Looking for docker-compose.yml in: {compose_file}")
            # Check if docker-compose.yml exists in the directory
            if os.path.isfile(compose_file):
                print(f"Starting containers in {dir_path}")
                # Start the docker-compose command in a separate process
                process = subprocess.Popen(
                    ["docker-compose", "-f", compose_file, "down"],
                    stdout=subprocess.PIPE,
                    stderr=subprocess.PIPE
                )
                # Store process handle for later use
                processes.append((dir_path, process))
            else:
                print(f"No docker-compose.yml found in {dir_path}")

    # Optional: Monitor all processes (not necessary for fire-and-forget but useful for logging)
    for dir_path, process in processes:
        stdout, stderr = process.communicate()
        if stdout:
            print(f"Output from {dir_path}:", stdout.decode())
        if stderr:
            print(f"Errors from {dir_path}:", stderr.decode())

if __name__ == "__main__":
    # You can specify a different directory here if needed
    start_containers(".")
