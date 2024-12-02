import { ProductDto } from "./product-dto";
import { User } from "./user";

export interface ReviewDto {
    id: number;
    userId: string;
    productId: number
    texto: string;
    rating: number;
    dateTime: string;
    user: User;
    product: ProductDto;
}
