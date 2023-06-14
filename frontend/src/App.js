import './App.css';
import MainPage from "./components/mainPage/MainPage";
import Orders from './pages/Orders/Orders';
import Wholesalers from './pages/Wholesalers/Wholesalers';
import Components from './pages/Components/Components';
import Dishes from './pages/Dishes/Dishes';
import NavigationRoute from './components/NavigationRoute';
import Users from './pages/Users/Users';
import Home from './pages/Home/Home'
import {
  BrowserRouter,
  Route,
  Routes,
} from "react-router-dom";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route element={<NavigationRoute />}>

          <Route path="/MainPage" element={<MainPage />} />
          <Route path="/orders" element={<Orders />} />
          <Route path="/wholesalers" element={<Wholesalers />} />
          <Route path="/components" element={<Components />} />
          <Route path="/users" element={<Users />} />
          <Route path="/dishes" element={<Dishes />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
