import { useEffect,useState } from "react"
import { Helmet } from "react-helmet";
import { getCategories } from "../../services/category";
import CategoryListItem from "../../components/Category/CategoryListItem";
import {Grid,Button} from "@mui/material";
import { NavLink } from "react-router-dom";


export default function Categories(){

    const [categories, setCategories] = useState(false)

    useEffect(() => {
           getCategories()
            .then(res =>{         
                if(res.ok && res.status === 200){ 
                    return res.json()
                }
            }).then(data => {
                setCategories(data.data)
            })
            .catch((error) => console.log("Error:" + error));
    }, [])  



    return(
        <>
            <Helmet>
                <title> Kategoriler</title>
            </Helmet>


    <div className="container mt-5">
        <h3> Kategoriler</h3>
        <Grid container justifyContent='end' className="mb-5">
            <Button variant="contained" color="primary" component={NavLink} to={`/categories/create`}>
                
                Yeni kategori ekle

            </Button>
        </Grid>    
            
            <Grid direction='row' container spacing={3}>
                

                {categories && categories.map((category, index) =>  (
                    
                    <CategoryListItem
                    key={index}
                        category={{id:category.id, 
                        name:category.name, 
                        isUsable:category.isUsable, 
                        imageUrl:category.imageUrl}} />
                    ))
                }
            </Grid>
        </div>
                          
        </>
    )
}
