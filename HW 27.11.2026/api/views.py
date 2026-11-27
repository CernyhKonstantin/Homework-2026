from rest_framework.response import Response
from rest_framework.request import Request
from rest_framework.views import APIView
from rest_framework import status
from rest_framework import viewsets

from django.core.exceptions import ObjectDoesNotExist



"""
APIView
    # CRUD
    READ
    def get(self, request)
    CREATE
    def post(self, request)
    UPDATE
    def put(self, request)
    DELETE
    def delte(self, request)
    Paartion update
    def patch(self, request)

"""
from .serializers import ProductSerializer, ProductModelSerializer
from .models import Product

class ProductViewSet(viewsets.ModelViewSet):
    queryset = Product.objects.all()
    serializer_class = ProductModelSerializer

    def get_queryset(self):
        products = super().get_queryset()

        order = self.request.query_params.get("order")

        if order == "asc":
            products = products.order_by("price")

        elif order == "desc":
            products = products.order_by("-price")

        return products
    def list(self, request, *args, **kwargs):
        print(request)
        return super().list(request, *args, **kwargs)

class ProductView(APIView):
    def get(self, request:Request, id):
        try:
            product = Product.objects.get(id=id)
            return Response({
                "status": status.HTTP_200_OK,
                "message":"Ok",
                "data":ProductSerializer(product).data
            }, status.HTTP_200_OK)
        except ObjectDoesNotExist:
            return Response({
                "status":status.HTTP_404_NOT_FOUND,
                "message":"Product not found",
                "data":None
            }, status.HTTP_404_NOT_FOUND)
            
    def put(self, request:Request, id):
        try:
            product = Product.objects.get(id=id)
            ser_prod = ProductSerializer(product, data=request.data)
            if ser_prod.is_valid(raise_exception=True):
                ser_prod.save()
                return Response({
                    'status': status.HTTP_200_OK,
                    "message":"Product successfuly was updated",
                    'data':ser_prod.data
                }, status=status.HTTP_200_OK)
            return Response({
                'status': status.HTTP_400_BAD_REQUEST,
                "message":"Bad Request",
                'data':ser_prod.errors
            }, status.HTTP_400_BAD_REQUEST)
        except ObjectDoesNotExist:
            return Response({
                "status":status.HTTP_404_NOT_FOUND,
                "message":"Product not found",
                "data":None
            }, status.HTTP_404_NOT_FOUND)

class ProductListView(APIView):
    def get(self, request:Request):
        order = request.query_params.get("order", None)
        if order:
            products = Product.objects.all().order_by("price" if 'asc' in order else "-price")
        else: products = Product.objects.all()  
        ser_prod = ProductSerializer(products, many=True)
        return Response(ser_prod.data, status=status.HTTP_200_OK)
    
    def post(self, request:Request):
        serializer = ProductSerializer(data=request.data)
        if serializer.is_valid(raise_exception=True):
            serializer.save()
            return Response({
                'status': status.HTTP_201_CREATED,
                "message":"Product successfuly was created",
                'data':serializer.data
                }, status=status.HTTP_201_CREATED)
        return Response({
            'status': status.HTTP_400_BAD_REQUEST,
            "message":"Bad Request",
            'data':serializer.errors
        }, status.HTTP_400_BAD_REQUEST)

class ExampleView(APIView):
    
    def get(self, request:Request):
        return Response({"username":"jack_nicolas", "email":"jacknicolas@django.com"},
                            status=status.HTTP_200_OK
                        )
        
    def post(self, request:Request):
        print("headers: ", request.content_type)
        print("POST: ", request.POST)
        print("body: ", request.data)
        r = Response()
        
        data = {
            "status": status.HTTP_200_OK,
            "message": "Ok",
            "data":request.data
        }
        
        r.data = data
        r.headers["Content-Type"]= request.content_type
        if request.data:
            body = request.data
            if body.get('name',None):
                r.headers["name"] = request.data['name']
        r.status_code = status.HTTP_200_OK
        # return Response({'status': status.HTTP_200_OK, 'message':'Ok'})
        return r

# Category API: collection endpoints (list/create) and detail endpoints
# (retrieve/full update/partial update/delete).
from .models import Category
from .serializers import CategorySerializer


class CategoryListCreateView(APIView):
    """GET /api/categories/ lists categories; POST creates one."""

    def get(self, request: Request):
        categories = Category.objects.all()
        serializer = CategorySerializer(categories, many=True)
        return Response(serializer.data, status=status.HTTP_200_OK)

    def post(self, request: Request):
        serializer = CategorySerializer(data=request.data)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_201_CREATED)


class CategoryDetailView(APIView):
    """GET, PUT, PATCH and DELETE /api/categories/<uuid:id>/ ."""

    @staticmethod
    def get_object(category_id):
        try:
            return Category.objects.get(id=category_id)
        except (Category.DoesNotExist, ValueError):
            return None

    def get(self, request: Request, category_id):
        category = self.get_object(category_id)
        if category is None:
            return Response({"detail": "Category not found."}, status=status.HTTP_404_NOT_FOUND)
        return Response(CategorySerializer(category).data, status=status.HTTP_200_OK)

    def put(self, request: Request, category_id):
        category = self.get_object(category_id)
        if category is None:
            return Response({"detail": "Category not found."}, status=status.HTTP_404_NOT_FOUND)
        serializer = CategorySerializer(category, data=request.data)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_200_OK)

    def patch(self, request: Request, category_id):
        category = self.get_object(category_id)
        if category is None:
            return Response({"detail": "Category not found."}, status=status.HTTP_404_NOT_FOUND)
        serializer = CategorySerializer(category, data=request.data, partial=True)
        serializer.is_valid(raise_exception=True)
        serializer.save()
        return Response(serializer.data, status=status.HTTP_200_OK)

    def delete(self, request: Request, category_id):
        category = self.get_object(category_id)
        if category is None:
            return Response({"detail": "Category not found."}, status=status.HTTP_404_NOT_FOUND)
        category.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)
