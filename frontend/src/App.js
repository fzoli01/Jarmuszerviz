
import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Main from './components/main/main';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Main/>}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
