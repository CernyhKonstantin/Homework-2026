from rest_framework import serializers

from .models import Category, Product


class CategorySerializer(serializers.ModelSerializer):
    class Meta:
        model = Category
        fields = ("id", "name")
        read_only_fields = ("id",)


class ProductModelSerializer(serializers.ModelSerializer):
    class Meta:
        model = Product
        fields = "__all__"


class ProductSerializer(serializers.Serializer):
    productId = serializers.UUIDField(source="id", read_only=True, required=False)
    name = serializers.CharField(max_length=255, required=False)
    price = serializers.FloatField(required=False)
    description = serializers.CharField(required=False, allow_blank=True, allow_null=True)
    price_usd = serializers.SerializerMethodField()
    full_info = serializers.SerializerMethodField()

    def get_full_info(self, instance: Product):
        return f"{instance.id}-{instance.name}-{instance.price}"

    def get_price_usd(self, instance: Product):
        rate = self.context.get("usd_rate", 45.4)
        return round(instance.price / rate, 2)

    def validate_price(self, value):
        if value <= 0:
            raise serializers.ValidationError("Price must be greater than zero.")
        return value

    def create(self, validated_data):
        return Product.objects.create(**validated_data)

    def update(self, instance: Product, validated_data):
        for field in ("name", "price", "description"):
            if field in validated_data:
                setattr(instance, field, validated_data[field])
        instance.save()
        return instance
