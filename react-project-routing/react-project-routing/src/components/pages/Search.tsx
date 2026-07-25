import {useNavigate, useSearchParams} from "react-router";

const Search = () =>
{
    const [searchParams, setSearchParams ] = useSearchParams()
    const navigate = useNavigate()
    return <div>Search list {searchParams.get("title")}  <button onClick={()=>{
        setSearchParams({title:"furniture"})
    }}>Change</button>
    <div><button onClick={()=>{
        navigate(`/subcategories/${searchParams.get("page")}`)
    }}>About</button></div>
    </div>;

}
export default Search;