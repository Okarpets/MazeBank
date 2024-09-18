import axios from 'axios';

export interface UserRequest {
    username: string;
    email: string;
    password: string;
}

export const getAllUsers = async () => {
    const response = await axios.get("https://localhost:7191/Home");
    return response.data;
};

export const createUser = async (username, email, password) => {
    try {
        const response = await axios.post("https://localhost:7191/Home", {
            username,
            email,
            passwordHash: password
        });
        console.log("User created successfully:", response.data);
    } catch (error) {
        console.error("Error creating user:", error.response?.data || error.message);
    }
};

// export const updateUser = async (userRequest: UserRequest) => {
//     await axios.put("https://localhost:7191/Home", userRequest);
// };
// TODO
// export const deleteUser = async (id: number) => {
//     await axios.delete(`https://localhost:7191/Home/${id}`);
// };


