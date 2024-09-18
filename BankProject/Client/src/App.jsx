import { useEffect, useState } from 'react'
import './App.css'
import { useTranslation } from 'react-i18next';
import { locales } from '../public/locales/locales';
import NavigationPanel from './NavigationPanel/NavigationPanel';
import ThemeProvider from './ThemeProvider/ThemeProvider';
import { getAllUsers, createUser } from './Services/users';

function App() {
    const [users, setUsers] = useState([]);
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    useEffect(() => {
      const fetchUsers = async () => {
        try {
          const users = await getAllUsers();
          setUsers(users);
        } catch (error) {
          console.error("Error fetching users:", error);
        }
      };
  
    fetchUsers();
  
      const intervalId = setInterval(fetchUsers, 5000); // Fetch every 5 seconds
  
      return () => clearInterval(intervalId);
    }, []);

  const handleCreateUser = async () => {
    try {
        await createUser(username, email, password);
    } catch (error) {
        console.error("Error creating user:", error);
    }
};

  return (
    <>
      <NavigationPanel/>
      <div>
        {users.map(user => (
          <div key={user.id}>{user.username}</div>
        ))}
      </div>

      <div>
        <h1>REGISTER</h1>
            <input 
                type="text" 
                value={username} 
                onChange={(e) => setUsername(e.target.value)} 
                placeholder="Username" 
            />
            <input 
                type="email" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} 
                placeholder="Email" 
            />
            <input 
                type="password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                placeholder="Password" 
            />
            <button onClick={handleCreateUser}>Create User</button>
        </div>



        <div>
          <h1>LOGIN</h1>
            <input 
                type="text" 
                value={username} 
                onChange={(e) => setUsername(e.target.value)} 
                placeholder="Username" 
            />
            <input 
                type="email" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} 
                placeholder="Email" 
            />
            <input 
                type="password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                placeholder="Password" 
            />
            <button onClick={handleCreateUser}>Create User</button>
        </div>

        <div>
          <h1>UPDATE</h1>
            <input 
                type="text" 
                value={username} 
                onChange={(e) => setUsername(e.target.value)} 
                placeholder="Username" 
            />
            <input 
                type="email" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} 
                placeholder="Email" 
            />
            <input 
                type="password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                placeholder="Password" 
            />
            <button onClick={handleCreateUser}>Create User</button>
        </div>

        <div>
          <h1>DELETE</h1>
            <input 
                type="text" 
                value={username} 
                onChange={(e) => setUsername(e.target.value)} 
                placeholder="Username" 
            />
            <input 
                type="email" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} 
                placeholder="Email" 
            />
            <input 
                type="password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                placeholder="Password" 
            />
            <button onClick={handleCreateUser}>Create User</button>
        </div>
    </>
  );
}

export default App